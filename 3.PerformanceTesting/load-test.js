import http from 'k6/http';
import { check, sleep} from 'k6';

export const options = {
    stages: [
        { duration: '1m', target: 50 },
        { duration: '2m', target: 50 },
        { duration: '1m', target: 0 },
    ],

    thresholds: {
        http_req_duration: ['p(99)<15'], 
    },
};

const BASE_URL = 'https://localhost:5001';

export default () => {
    const customers = http.get(`${BASE_URL}/customers/`).json();
    check(customers, { 'retrieved customers': (obj) => obj.customers.length > 0 });
    sleep(1);
};
