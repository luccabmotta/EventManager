import http from 'k6/http';
import { check } from 'k6';

const BASE_URL = 'https://localhost:5001/api/v1';

export let options = {
    vus: 100,
    duration: '60s',
};

const EVENT_ID = 51;

export default function () {

    const patchEventPayload = JSON.stringify({
        name: 'Evento Atualizado',
        location: 'Novo Local',
        date: '2025-04-20T20:00:00',
    });

    const patchEventRes = http.patch(`${BASE_URL}/events/${EVENT_ID}`, patchEventPayload, {
        headers: { 'Content-Type': 'application/json' },
    });
}
