import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = {
    vus: 10,
    duration: '30s',
};

const BASE_URL = 'https://localhost:7010/api/v1';

export function createArtist() {

    const artistPayload = JSON.stringify({
        name: 'Novo Artista',
        email: 'artista@exemplo.com',
    });

    let res = http.post(`${BASE_URL}/artists`, artistPayload, {
        headers: { 'Content-Type': 'application/json' },
    });

    check(res, {
        'POST /artists status 201': (r) => r.status === 201,
        'POST /artists tem corpo': (r) => r.body.length > 0,
    });

    console.log('Resposta do POST /artists:', res.body);
}

export function createEvent() {

    const eventPayload = JSON.stringify({
        name: 'Novo Evento',
        location: 'Local do Evento',
        date: '2025-04-01T00:00:00',
    });

    let res = http.post(`${BASE_URL}/events`, eventPayload, {
        headers: { 'Content-Type': 'application/json' },
    });

    check(res, {
        'POST /events status 201': (r) => r.status === 201,
        'POST /events tem corpo': (r) => r.body.length > 0,
    });

    console.log('Resposta do POST /events:', res.body);
}

export default function () {
    createArtist();
    createEvent();
    sleep(1);
}
