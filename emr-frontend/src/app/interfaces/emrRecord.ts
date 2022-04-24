export interface EMRRecord {
    symptom: {
        description: string,
        body_area: string,
        observer_SSN: string
    },
    diagnosis: {
        condition: string,
        date: {
            year: number,
            month: number,
            day: number
        }
    },
    prescription: {
        mname: string,
        dosage: string
    }
}