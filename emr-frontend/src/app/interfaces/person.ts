export interface Person {
    ssn: number,
    phone_no: number,
    address: string,
    sex: string,
    name: {
        f_name: string,
        l_name: string
    },
    dob: {
        year: number,
        month: number,
        day: number
    }
}