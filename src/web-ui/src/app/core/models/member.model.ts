export interface Member {
    id: number,
    email: string,
    dateOfBirth: string,
    gender: Gender,
    name: string,
    briefDescription?: string,
    lookingFor?: string,
    interests?: string,
    city?: string,
    country?: string,
    photos: Photo[],
    created: Date,
    lastActive: Date
}

export interface MemberSummary {
    id: number,
    name: string,
    city?: string,
    mainPhotoUrl?: string
}

export const enum Gender {
    Male = "Male",
    Female = "Female"
}

export interface Photo {
    name: string,
    url: string,
    isMain: boolean
}