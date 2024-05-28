import http from 'k6/http';

export const options = {
  vus: 100,
  duration: '60s',
};

export default function () {
  const payload = JSON.stringify({
    Username: 'test@test.com',
    Password: 'Test123',
  });
  const headers = { 'Content-Type': 'application/json' };
  http.post('http://dockerdummy.io/api/authentication/login', payload, { headers });
}