import http from 'k6/http';
import { check, group } from 'k6';
import { Trend } from 'k6/metrics';

const BASE_URL = 'https://localhost:7010/api/v1';

let deleteArtistTrend = new Trend('delete_artist_duration');
let deleteEventTrend = new Trend('delete_event_duration');

export let options = {
    vus: 10,
    duration: '30s',
};

export default function () {
    let artistId = null;
    let eventId = null;

    group('Criação dos recursos (POST)', () => {
        const artistPayload = JSON.stringify({
            name: 'Artista Teste K6',
            email: 'testek6@exemplo.com',
        });

        const postArtistRes = http.post(`${BASE_URL}/artists`, artistPayload, {
            headers: { 'Content-Type': 'application/json' },
        });

        if (postArtistRes.status === 201) {
            try {
                artistId = JSON.parse(postArtistRes.body).id;
            } catch (e) {
                console.warn('❌ Erro ao parsear resposta do POST /artists');
            }
        }

        const eventPayload = JSON.stringify({
            name: 'Evento Teste K6',
            location: 'Auditório de Testes',
            date: '2025-04-10T20:00:00',
        });

        const postEventRes = http.post(`${BASE_URL}/events`, eventPayload, {
            headers: { 'Content-Type': 'application/json' },
        });

        if (postEventRes.status === 201) {
            try {
                eventId = JSON.parse(postEventRes.body).id;
            } catch (e) {
                console.warn('❌ Erro ao parsear resposta do POST /events');
            }
        }
    });

    group('Remoção dos recursos (DELETE)', () => {
        if (artistId) {
            const res = http.del(`${BASE_URL}/artists/${artistId}`);
            deleteArtistTrend.add(res.timings.duration); 
        }

        if (eventId) {
            const res = http.del(`${BASE_URL}/events/${eventId}`);
            deleteEventTrend.add(res.timings.duration); 
        }
    });
}
