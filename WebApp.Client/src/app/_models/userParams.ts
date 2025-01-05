import { Member } from './member'; // Adjust the import path as necessary

export class UserParams
{
    //phoneNumber: string | undefined;
    gender : number | undefined;
    //firstName:string | undefined;
    //lastName:string | undefined;
    pageNumber = 1;
    pageSize = 10;
    plan:string | undefined;
    planStatus:number | undefined;
    
    // constructor(Member: Member | null)
    // {
    //     this.gender = Member?.gender === 0? 1:0;
    // }
}