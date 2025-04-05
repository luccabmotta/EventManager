import http from 'k6/http';
import { sleep } from 'k6';

export let options = {
    vus: 10,
    duration: '30s',
};

const BASE_URL = 'https://localhost:7010/api/v1';

export function updateArtist() {
    const artistPayload = JSON.stringify({
        name: 'Artista Atualizado',
        email: 'artista_atualizado@exemplo.com',
    });

    let res = http.put(`${BASE_URL}/artists/903`, artistPayload, {
        headers: { 'Content-Type': 'application/json' },
    });
}

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
    updateArtist();
    updateEvent();
    sleep(1);
}
