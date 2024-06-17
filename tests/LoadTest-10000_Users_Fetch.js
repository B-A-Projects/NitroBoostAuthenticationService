import http from 'k6/http';

export const options = {
  vus: 10000,
  duration: '180s',
};

export default function () {
  http.get('http://dockerdummy.io/api/authentication');
}