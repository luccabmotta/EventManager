import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = {
    vus: 10, // 10 usuários virtuais simultâneos
    duration: '20s', // Tempo total de execução do teste
};

const BASE_URL = 'https://localhost:7010/api/events'; // Substitua pelo endpoint real

export default function () {
    let getResponse = http.get(BASE_URL);
    check(getResponse, {
        'GET /api/events status é 200': (res) => res.status === 200,
    });

    let eventId = null;

    let payload = JSON.stringify({
        name: `Evento Teste ${__VU}-${__ITER}`,
        description: 'Descrição do evento gerado pelo k6',
        date: '2025-06-15T18:00:00',
    });

    let params = {
        headers: { 'Content-Type': 'application/json' },
    };

    let postResponse = http.post(BASE_URL, payload, params);
    check(postResponse, {
        'POST /api/events status é 201': (res) => res.status === 201,
    });

    if (postResponse.status === 201) {
        eventId = JSON.parse(postResponse.body).id; // Pega o ID do evento criado
    }

    if (eventId) {
        let getSingleResponse = http.get(`${BASE_URL}/${eventId}`);
        check(getSingleResponse, {
            'GET /api/events/{id} status é 200': (res) => res.status === 200,
        });
    }

    if (eventId) {
        let deleteResponse = http.del(`${BASE_URL}/${eventId}`);
        check(deleteResponse, {
            'DELETE /api/events/{id} status é 204': (res) => res.status === 204,
        });
    }

    if (eventId) {
        let deleteResponse = http.del(`${BASE_URL}/${eventId}`);
        check(deleteResponse, {
            'DELETE /api/events/{id} status é 204': (res) => res.status === 204,
        });
    }

    if (eventId) {
        let deleteResponse = http.del(`${BASE_URL}/${eventId}`);
        check(deleteResponse, {
            'DELETE /api/events/{id} status é 204': (res) => res.status === 204,
        });
    }

    if (eventId) {
        let deleteResponse = http.del(`${BASE_URL}/${eventId}`);
        check(deleteResponse, {
            'DELETE /api/events/{id} status é 204': (res) => res.status === 204,
        });
    }

    if (eventId) {
        let deleteResponse = http.del(`${BASE_URL}/${eventId}`);
        check(deleteResponse, {
            'DELETE /api/events/{id} status é 204': (res) => res.status === 204,
        });
    }


    if (eventId) {
        let deleteResponse = http.del(`${BASE_URL}/${eventId}`);
        check(deleteResponse, {
            'DELETE /api/events/{id} status é 204': (res) => res.status === 204,
        });
    }

    if (eventId) {
        let deleteResponse = http.del(`${BASE_URL}/${eventId}`);
        check(deleteResponse, {
            'DELETE /api/events/{id} status é 204': (res) => res.status === 204,
        });
    }

    if (eventId) {
        let deleteResponse = http.del(`${BASE_URL}/${eventId}`);
        check(deleteResponse, {
            'DELETE /api/events/{id} status é 204': (res) => res.status === 204,
        });
    }

    if (eventId) {
        let deleteResponse = http.del(`${BASE_URL}/${eventId}`);
        check(deleteResponse, {
            'DELETE /api/events/{id} status é 204': (res) => res.status === 204,
        });
    }

    sleep(1); // Aguarda 1 segundo antes da próxima iteração
}
