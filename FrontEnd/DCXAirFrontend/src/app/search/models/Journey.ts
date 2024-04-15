import { Flight } from "./Flight";


export interface Journey {
    origin: string;
    destination: string;
    price: string;
    routeType: boolean;
    flights: Flight[];
}
