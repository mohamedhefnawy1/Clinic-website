import { EMRRecord } from './emrRecord';
import { Person } from './person';

export interface Patient extends Person {
    emr?: {
        p_ssn: number,
        emr_id: string,
        record: EMRRecord[]
    }
}