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

    let artistId = null;

    group('Criação dos recursos (POST)', () => {

        const artistPayload = JSON.stringify({
            name: 'Artista Teste K6',
            email: 'testek6@exemplo.com',
        });

        const postArtistRes = http.post(`${BASE_URL}/artists`, artistPayload, {
            headers: { 'Content-Type': 'application/json' },
            tags: { name: 'AddArtist' }
        });

        if (postArtistRes.status === 201) {
            try {
                artistId = JSON.parse(postArtistRes.body).id;
            } catch (e) {
                console.warn('❌ Erro ao parsear resposta do POST /artists');
            }
        }
    });

    group('Remoção dos recursos (DELETE)', () => {
        if (artistId) {
            const res = http.del(`${BASE_URL}/artists/${artistId}`, null, {
                tags: { name: 'DeleteArtist' }
            });
        }
    });
}
