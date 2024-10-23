import http from 'k6/http';
import { check, sleep} from 'k6';

export const options = {
    stages: [
        { duration: '2m', target: 400 },
        { duration: '1h56m', target: 400 },
        { duration: '2m', target: 0 }
    ],

    thresholds: {
        http_req_duration: ['p(99)<15'], 
    },
};

const BASE_URL = 'https://localhost:5001';

export default () => {
    const responses = http.batch([
        ['GET', `${BASE_URL}/customers/`, null],
        ['GET', `${BASE_URL}/customers/`, null],
        ['GET', `${BASE_URL}/customers/`, null],
        ['GET', `${BASE_URL}/customers/`, null],
    ]);

    responses.forEach(x => {
        const customers = x.json();
        check(customers, { 'retrieved customers': (obj) => obj.customers.length > 0 });
    })    
    sleep(1);
};
