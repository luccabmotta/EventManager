import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = {
    vus: 10,
    duration: '30s', 
};

const BASE_URL = 'https://localhost:7010/api/v1';

export default function () {

    let resArtists = http.get(`${BASE_URL}/artists?page=1&pageSize=50`);

    check(resArtists, {
        'GET /artists status 200': (r) => r.status === 200,
        'GET /artists has items': (r) => JSON.parse(r.body).items.length > 0,
    });

    let resArtistById = http.get(`${BASE_URL}/artists/1`); 

    check(resArtistById, {
        'GET /artists/{id} status 200 or 404': (r) => r.status === 200 || r.status === 404,
    });

    let resevents = http.get(`${base_url}/events?page=1&pagesize=50`);

    check(resevents, {
        'get /events status 200': (r) => r.status === 200
    });

    let reseventbyid = http.get(`${base_url}/events/1`);

    check(reseventbyid, {
        'get /events/{id} status 200 or 404': (r) => r.status === 200 || r.status === 404,
    });

    sleep(1);
}
