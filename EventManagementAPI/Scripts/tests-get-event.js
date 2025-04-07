import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = {
    vus: 100,
    duration: '60s', 
};

const BASE_URL = 'https://localhost:5001/api/v1';

export default function () {

    let resevents = http.get(`${BASE_URL}/events?page=1&pagesize=50`);

    let reseventbyid = http.get(`${BASE_URL}/events/44`);

}
