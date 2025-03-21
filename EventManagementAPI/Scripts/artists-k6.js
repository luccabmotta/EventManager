import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = {
    vus: 10, // 10 usuários virtuais simultâneos
    duration: '30s', // Tempo total de execução do teste
};

const BASE_URL = 'https://localhost:7010/api/artists'; // Substitua pelo endpoint real

export default function () {
    // GET - Buscar todos os artistas
    let getResponse = http.get(BASE_URL);
    check(getResponse, {
        'GET /api/artists status é 200': (res) => res.status === 200,
    });

    let artistId = null;

    // POST - Criar um novo artista
    let payload = JSON.stringify({
        name: `Artista Teste ${__VU}-${__ITER}`,
        genre: 'Rock',
        description: 'Descrição do artista gerado pelo k6',
        phone: '123456789',
        email: `artista${__VU}-${__ITER}@teste.com`,
    });

    let params = {
        headers: { 'Content-Type': 'application/json' },
    };

    let postResponse = http.post(BASE_URL, payload, params);
    check(postResponse, {
        'POST /api/artists status é 201': (res) => res.status === 201,
    });

    if (postResponse.status === 201) {
        artistId = JSON.parse(postResponse.body).id; // Pega o ID do artista criado
    }

    // GET - Buscar um artista específico
    if (artistId) {
        let getSingleResponse = http.get(`${BASE_URL}/${artistId}`);
        check(getSingleResponse, {
            'GET /api/artists/{id} status é 200': (res) => res.status === 200,
        });
    }

    // DELETE - Remover o artista criado
    if (artistId) {
        let deleteResponse = http.del(`${BASE_URL}/${artistId}`);
        check(deleteResponse, {
            'DELETE /api/artists/{id} status é 204': (res) => res.status === 204,
        });
    }

    sleep(1); // Aguarda 1 segundo antes da próxima iteração
}
