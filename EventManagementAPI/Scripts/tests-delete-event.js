import http from 'k6/http';
import { check, group } from 'k6';
import { Trend } from 'k6/metrics';

const BASE_URL = 'https://localhost:5001/api/v1';

let deleteTrend = new Trend('delete_duration');

export let options = {
    vus: 100,
    duration: '60s',
};

export default function () {

    let eventId = null;

    group('Criação dos recursos (POST)', () => {

        const eventPayload = JSON.stringify({
            name: 'Evento Teste K6',
            location: 'Auditório de Testes',
            date: '2025-04-10T20:00:00',
        });

        const postEventRes = http.post(`${BASE_URL}/events`, eventPayload, {
            headers: { 'Content-Type': 'application/json' },
            tags: { name: 'CreateEvent' }
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
        if (eventId) {
            const res = http.del(`${BASE_URL}/events/${eventId}`, null, {
                tags: { name: 'DeleteEvent' }
            });
        }
    });
}
