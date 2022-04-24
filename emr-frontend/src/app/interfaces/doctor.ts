import { Person } from './person';

export interface Doctor extends Person {
    field: string,
    clinic: {
        location: string,
        name: string
    },
    educationalBackground: {
        school: string,
        years: number
    }
}