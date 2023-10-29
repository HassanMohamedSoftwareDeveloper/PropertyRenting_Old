import { Component, OnInit, ViewChild } from "@angular/core";
import {
    AbstractControl,
    FormBuilder,
    FormControl,
    FormGroup,
    Validators,
} from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { v4 as uuidv4 } from "uuid";
import { ModalComponent } from "../../../CustomTemplates/modal/modal.component";
import { Breadcrumb } from "../../../Models/breadcrumb";
import { City } from "../../../Models/city";
import { ContactPerson } from "../../../Models/contact-person";
import { Country } from "../../../Models/country";
import { Enum } from "../../../Models/enum";
import { Nationality } from "../../../Models/nationality";
import { Renter } from "../../../Models/renter";
import { AlertifyService } from "../../../Services/alertify.service";
import { BreadcrumbService } from "../../../Services/breadcrumb.service";
import { CityService } from "../../../Services/city.service";
import { CountryService } from "../../../Services/country.service";
import { DialogService } from "../../../Services/dialog.service";
import { NationalityService } from "../../../Services/nationality.service";
import { RenterService } from "../../../Services/renter.service";
import { TranslationService } from "../../../Services/translation.service";

@Component({
    selector: "app-renter-add-update",
    templateUrl: "./renter-add-update.component.html",
    styleUrls: ["./renter-add-update.component.css"],
})
export class RenterAddUpdateComponent implements OnInit {
    breadcrumbItems: Breadcrumb[] = [];
    renterId: any = null;
    renterForm!: FormGroup;
    renter: Renter = {
        countryId: null,
        cityId: null,
        contactPersons: [],
    };
    contactPerson: ContactPerson = {};
    @ViewChild("modal", { static: false }) modal?: ModalComponent;

    submitted = false;
    status = "1";
    blackList = "0";
    renterType = "";
    genderTypeId = "";

    identityTypes: Enum[] = [];
    renterTypes: Enum[] = [];

    countries: Country[] = [];
    cities: City[] = [];
    citiesList: City[] = [];
    nationalities: Nationality[] = [];

    //#region Collapse :
    basicInfoCollapsed = false;
    identityInfoCollapsed = false;
    addressInfoCollapsed = false;
    personInfoCollapsed = false;
    contactPersonInfoCollapsed = false;
    otherInfoCollapsed = false;
    isCollapsed = false;
    //#endregion

    constructor(
        private fb: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private alertify: AlertifyService,
        private translateService: TranslationService,
        private countryService: CountryService,
        private cityService: CityService,
        private renterService: RenterService,
        private nationalityService: NationalityService,
        private breadcrumbService: BreadcrumbService,
        private dialogService: DialogService
    ) {
        this.identityTypes = renterService.IdentityTypes;
        this.renterTypes = renterService.RenterTypes;
    }

    ngOnInit(): void {
        this.createForm();
        this.loadCountries();
        this.loadCities();
        this.loadNationalities();
        this.breadcrumbItems = this.breadcrumbService.RenterDetailsItems;
        const id = this.route.snapshot.paramMap.get("id");
        if (id) {
            this.getRenterById(id);
        }
    }

    createForm() {
        this.renterForm = this.fb.group({
            BasicInfo: this.fb.group({
                Status: ["1", Validators.required],
                RenterType: [null, Validators.required],
                NameAR: [null, Validators.required],
                NameEN: [null],
            }),
            IdentityInfo: this.fb.group({
                NationalityId: [
                    null,
                    [Validators.required, this.validateDropdown],
                ],
                IdentityType: [null, Validators.required],
                IdentityNumber: [null, Validators.required],
                IdentityIssuePlace: [null, Validators.required],
                IdentityIssueDate: [null, Validators.required],
                IdentityExpiryDate: [null, Validators.required],
            }),
            AddressInfo: this.fb.group({
                CountryId: [null, [Validators.required, this.validateDropdown]],
                CityId: [null, [Validators.required, this.validateDropdown]],
                RegionCode: [null],
                PostalCode: [null],
            }),
            PersonInfo: this.fb.group({
                Email: [null, [Validators.email]],
                PhoneNumber: [null],
                PhoneNumber2: [null],
                MobileNumber: [null],
                MobileNumber2: [null],
                Fax: [null],
                GuarantorName: [null],
                GuarantorMobileNumber: [null],
                GuarantorAddress: [null],
            }),
            OtherInfo: this.fb.group({
                Gender: [null, Validators.required],
                BlackList: ["0"],
                Notes: [null],
            }),
        });
    }

    getRenterById(id: string) {
        this.renterService.GetById(id).subscribe(
            (result) => {
                this.renter = result;
                this.status = this.renter.status === true ? "1" : "0";
                this.renterType = String(this.renter.typeId || "");
                this.blackList = this.renter.isBlackListed === true ? "1" : "0";
                this.genderTypeId = String(this.renter.genderTypeId || "");
                this.getCitiesByCountryId(this.renter.countryId);
            },
            (error) => console.log(error)
        );
    }

    loadCountries() {
        this.countryService.GetAllCountries().subscribe(
            (result) => {
                this.countries = result;
            },
            (error) => console.log(error)
        );
    }
    loadCities() {
        this.cityService.GetAllCities().subscribe(
            (result) => {
                this.citiesList = result;
            },
            (error) => console.log(error)
        );
    }
    getCitiesByCountryId(countryId: any) {
        this.cities = this.citiesList.filter((c) => c.countryId === countryId);
    }
    loadNationalities() {
        this.nationalityService.GetAll().subscribe(
            (result) => {
                this.nationalities = result;
            },
            (error) => console.log(error)
        );
    }

    //#region Getters :
    get BasicInfo() {
        return this.renterForm.controls["BasicInfo"] as FormGroup;
    }
    get IdentityInfo() {
        return this.renterForm.controls["IdentityInfo"] as FormGroup;
    }
    get AddressInfo() {
        return this.renterForm.controls["AddressInfo"] as FormGroup;
    }
    get PersonInfo() {
        return this.renterForm.controls["PersonInfo"] as FormGroup;
    }
    get OtherInfo() {
        return this.renterForm.controls["OtherInfo"] as FormGroup;
    }

    get Status() {
        return this.BasicInfo.controls["Status"] as FormControl;
    }
    get RenterType() {
        return this.BasicInfo.controls["RenterType"] as FormControl;
    }
    get NameAR() {
        return this.BasicInfo.controls["NameAR"] as FormControl;
    }

    get NationalityId() {
        return this.IdentityInfo.controls["NationalityId"] as FormControl;
    }
    get IdentityType() {
        return this.IdentityInfo.controls["IdentityType"] as FormControl;
    }
    get IdentityNumber() {
        return this.IdentityInfo.controls["IdentityNumber"] as FormControl;
    }
    get IdentityIssuePlace() {
        return this.IdentityInfo.controls["IdentityIssuePlace"] as FormControl;
    }
    get IdentityIssueDate() {
        return this.IdentityInfo.controls["IdentityIssueDate"] as FormControl;
    }
    get IdentityExpiryDate() {
        return this.IdentityInfo.controls["IdentityExpiryDate"] as FormControl;
    }

    get CountryId() {
        return this.AddressInfo.controls["CountryId"] as FormControl;
    }
    get CityId() {
        return this.AddressInfo.controls["CityId"] as FormControl;
    }

    get Email() {
        return this.PersonInfo.controls["Email"] as FormControl;
    }

    get Gender() {
        return this.OtherInfo.controls["Gender"] as FormControl;
    }
    //#endregion

    //#region Collapse :
    get checkAllCollapsed() {
        this.isCollapsed =
            this.basicInfoCollapsed &&
            this.identityInfoCollapsed &&
            this.personInfoCollapsed &&
            this.addressInfoCollapsed &&
            this.contactPersonInfoCollapsed &&
            this.otherInfoCollapsed;

        return this.isCollapsed;
    }
    toggleCollabse() {
        this.isCollapsed = !this.isCollapsed;

        this.basicInfoCollapsed =
            this.identityInfoCollapsed =
            this.addressInfoCollapsed =
            this.personInfoCollapsed =
            this.contactPersonInfoCollapsed =
            this.otherInfoCollapsed =
                this.isCollapsed;
    }
    //#endregion

    onSubmit() {
        this.submitted = true;
        if (this.renterForm.invalid) {
            return;
        }
        this.renter.status = this.status === "1" ? true : false;
        this.renter.typeId = +this.renterType;
        this.renter.isBlackListed = this.blackList === "1" ? true : false;
        this.renter.genderTypeId = +this.genderTypeId;

        console.log(this.renter);
        if (this.renter.id == null) {
            this.Add();
        } else {
            this.Update();
        }
    }

    private Add() {
        this.renter.id = uuidv4();
        for (const contact of this.renter.contactPersons || []) {
            contact.id = uuidv4();
        }
        this.renterService.Create(this.renter).subscribe(
            () => {
                const successMsg =
                    this.translateService.Translate("AddedSuccessfully");
                this.alertify.success(successMsg);
                this.backToList();
            },
            (error) => {
                console.log(error);
                this.renter.id = null;
            }
        );
    }
    private Update() {
        for (const contact of this.renter.contactPersons || []) {
            if (contact.id === null) {
                contact.id = uuidv4();
            }
        }
        this.renterService.Edit(this.renter.id, this.renter).subscribe(
            () => {
                const successMsg = this.translateService.Translate(
                    "UpdatedSuccessfully"
                );
                this.alertify.success(successMsg);
                this.backToList();
            },
            (error) => console.log(error)
        );
    }

    resetFrom() {
        this.renterForm.reset();
        this.submitted = false;
        this.status = "1";
        this.blackList = "0";
        this.renterType = "";
    }

    backToList() {
        this.router.navigate(["/definitions/renters"]);
    }

    addContactPerson() {
        this.contactPerson = { id: null, tempId: null, status: true };
        this.modal?.showModal();
    }
    editContactPerson(contactPerson: ContactPerson) {
        this.contactPerson = { ...contactPerson };
        this.modal?.showModal();
    }

    deleteContactPerson(contactPerson: ContactPerson) {
        const deleteMsg = this.translateService.Translate("DeleteMessage");
        this.dialogService.confirm(deleteMsg).subscribe((res) => {
            if (res) {
                this.renter.contactPersons =
                    this.renter?.contactPersons?.filter(
                        (x) =>
                            (x.id != null && x.id != contactPerson.id) ||
                            (x.tempId != null &&
                                x.tempId != contactPerson.tempId)
                    );
            }
        });
    }
    HideModal() {
        this.modal?.hideModal();
    }
    addUpdateContactPerson(data: ContactPerson) {
        this.HideModal();
        this.renter.contactPersons = this.renter.contactPersons || [];

        if (data.id != null) {
            const index = this.renter.contactPersons.findIndex(
                (x) => x.id == data.id
            );
            if (index > -1) {
                this.renter.contactPersons[index] = { ...data };
            }
        } else {
            const index = this.renter.contactPersons.findIndex(
                (x) => x.tempId == data.tempId
            );
            if (index === -1) {
                this.renter.contactPersons.push({ ...data });
            } else {
                this.renter.contactPersons[index] = { ...data };
            }
        }
    }

    get HasData() {
        const len = this.renter.contactPersons?.length || 0;
        return len > 0;
    }

    validateDropdown(control: AbstractControl) {
        const thisValue = control.value;
        if (
            thisValue == undefined ||
            thisValue == null ||
            thisValue == "" ||
            thisValue == "null"
        ) {
            return { required: true };
        }
        return null;
    }
}
