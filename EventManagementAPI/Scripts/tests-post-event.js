import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = {
    vus: 100,
    duration: '60s',
};

const BASE_URL = 'https://localhost:5001/api/v1';

export function createEvent() {

    const eventPayload = JSON.stringify({
        name: 'Novo Evento',
        location: 'Local do Evento',
        date: '2025-04-01T00:00:00',
    });

    let res = http.post(`${BASE_URL}/events`, eventPayload, {
        headers: { 'Content-Type': 'application/json' },
    });
}

export default function () {
    createEvent();
}
