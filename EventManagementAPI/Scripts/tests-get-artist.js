import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = {
    vus: 100,
    duration: '60s', 
};

const BASE_URL = 'https://localhost:5001/api/v1';

export default function () {

    let resArtists = http.get(`${BASE_URL}/artists?page=1&pageSize=50`);

    let resArtistById = http.get(`${BASE_URL}/artists/903`); 

}
