import http from 'k6/http';
import { sleep } from 'k6';

export let options = {
    vus: 100,
    duration: '60s',
};

const BASE_URL = 'https://localhost:5001/api/v1';

export function updateEvent() {
    const eventPayload = JSON.stringify({
        name: 'Evento Atualizado',
        location: 'Novo Local do Evento',
        date: '2025-05-01T00:00:00',
    });

    let res = http.put(`${BASE_URL}/events/44`, eventPayload, {
        headers: { 'Content-Type': 'application/json' },
    });
}

export default function () {
    updateEvent();
}
