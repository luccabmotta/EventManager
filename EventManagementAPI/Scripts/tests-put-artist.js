import http from 'k6/http';
import { sleep } from 'k6';

export let options = {
    vus: 100,
    duration: '60s',
};

const BASE_URL = 'https://localhost:5001/api/v1';

export function updateArtist() {
    const artistPayload = JSON.stringify({
        name: 'Artista Atualizado',
        email: 'artista_atualizado@exemplo.com',
    });

    let res = http.put(`${BASE_URL}/artists/903`, artistPayload, {
        headers: { 'Content-Type': 'application/json' },
    });
}

export default function () {
    updateArtist();
}
