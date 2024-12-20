import { Gym } from "./gym";

export interface Supplement
{
        readonly supplementId: number;
        name: string;
        price: number;
        weight: string;
        description?: string;
        imageUrl?: string;
        gym: Gym;
}