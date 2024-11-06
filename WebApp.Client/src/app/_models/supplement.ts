import { Gym } from "./gym";

export interface Supplement
{
        supplementId: number;
        name: string;
        price: number;
        weight: string;
        description?: string;
        imageUrl?: string;
        gym: Gym;
}