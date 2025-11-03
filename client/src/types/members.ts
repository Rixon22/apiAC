export interface Member {
    id: string;
    birthDate: string;
    imageUrl?: string;
    displayName: string;
    created: string;
    lastActive: string;
    gender: string;
    description?: string;
    city: string;
    country: string;
}

export interface Photo {
    imageUrl: any;
    id: number;
    publicId?: string;
    memberId: string;
}