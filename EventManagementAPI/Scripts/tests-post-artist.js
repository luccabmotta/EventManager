import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = {
    vus: 100,
    duration: '60s',
};

const BASE_URL = 'https://localhost:5001/api/v1';

export function createArtist() {

    const artistPayload = JSON.stringify({
        name: 'Novo Artista',
        email: 'artista@exemplo.com',
    });

    let res = http.post(`${BASE_URL}/artists`, artistPayload, {
        headers: { 'Content-Type': 'application/json' },
    });

}
export default function () {
    createArtist();
}
