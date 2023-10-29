import { ContactPerson } from "./contact-person";

export interface Renter {
    id?: any;
    status?: boolean;
    typeId?: number;
    nameAR?: string;
    nameEN?: string;
    nationalityId?: any;
    identityTypeId?: number;
    identityNumber?: string;
    identityIssuePlace?: string;
    identityIssueDate?: Date;
    identityExpiryDate?: Date;
    cityId?: any;
    regionCode?: string;
    postalCode?: string;
    email?: string;
    phone1?: string;
    phone2?: string;
    mobile1?: string;
    mobile2?: string;
    fax?: string;
    guarantorName?: string;
    guarantorPhone?: string;
    guarantorAddress?: string;
    genderTypeId?: number;
    isBlackListed?: boolean;
    notes?: string;
    countryId?: any;
    country?: string;
    city?: string;
    nationality?: string;
    contactPersons?: ContactPerson[];
}
