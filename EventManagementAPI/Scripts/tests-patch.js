import http from 'k6/http';
import { check } from 'k6';

const BASE_URL = 'https://localhost:7010/api/v1';

export let options = {
    vus: 10,
    duration: '30s',
};

const ARTIST_ID = 902;
const EVENT_ID = 51;

export default function () {

    const patchArtistPayload = JSON.stringify({
        name: 'Artista Atualizado',
        email: 'novoemail@exemplo.com',
    });

    const patchArtistRes = http.patch(`${BASE_URL}/artists/${ARTIST_ID}`, patchArtistPayload, {
        headers: { 'Content-Type': 'application/json' },
    });

    check(patchArtistRes, {
        'PATCH /artists status 204 ou 200': (r) => r.status === 204 || r.status === 200,
    });

    const patchEventPayload = JSON.stringify({
        name: 'Evento Atualizado',
        location: 'Novo Local',
        date: '2025-04-20T20:00:00',
    });

    const patchEventRes = http.patch(`${BASE_URL}/events/${EVENT_ID}`, patchEventPayload, {
        headers: { 'Content-Type': 'application/json' },
    });

    check(patchEventRes, {
        'PATCH /events status 204 ou 200': (r) => r.status === 204 || r.status === 200,
    });
}
