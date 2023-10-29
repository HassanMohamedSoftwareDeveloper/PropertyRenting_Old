export interface Account {
    id?: any;
    code?: string;
    nameAR?: string;
    nameEN?: string;
    accountTypeId?: number;
    parentId?: any;
    level?: number;
    accountChildren?: Account[];
    hasChildren?: boolean;
}
