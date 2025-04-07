import http from 'k6/http';
import { check } from 'k6';

const BASE_URL = 'https://localhost:5001/api/v1';

export let options = {
    vus: 100,
    duration: '60s',
};

const ARTIST_ID = 902;

export default function () {

    const patchArtistPayload = JSON.stringify({
        name: 'Artista Atualizado',
        email: 'novoemail@exemplo.com',
    });

    const patchArtistRes = http.patch(`${BASE_URL}/artists/${ARTIST_ID}`, patchArtistPayload, {
        headers: { 'Content-Type': 'application/json' },
    });
}
