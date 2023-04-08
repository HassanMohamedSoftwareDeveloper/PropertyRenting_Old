import { BuildingContributer } from "./building-contributer";

export interface Building {
    id?: any;
    symbolNo?: number;
    status?: boolean;
    name?: string;
    typeId?: number;
    type?: string;
    employeeId?: any;
    districtId?: any;
    addressAR?: string;
    addressEN?: string;
    location?: string;
    latitude?: string;
    longtude?: string;
    constructionStatusId?: number;
    constructionStatus?: string;
    establisYear?: number;
    totalArea?: number;
    rentableArea?: number;
    yearRentAmount?: number;
    yearReRentAmount?: number;
    levelNo?: number;
    unitsNo?: number;
    receiveDate?: Date;

    employee?: string;
    district?: string;
    cityId?: any;
    city?: string;
    countryId?: any;
    country?: string;
    notes?: string;
    contributers?: BuildingContributer[];
}
