import { Gender } from '../../models/member.model';

export interface IUserLoginRequest {
    email: string,
    password: string
}

export interface IUser {
    id: number,
    photoUrl: string
}

export interface IUserRegistrationRequest {
    email: string,
    name: string,
    gender: Gender,
    dateOfBirth: string,
    password: string
}