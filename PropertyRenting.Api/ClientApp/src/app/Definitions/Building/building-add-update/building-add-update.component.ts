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
import { Building } from "../../../Models/building";
import { BuildingContributer } from "../../../Models/building-contributer";
import { Enum } from "../../../Models/enum";
import { AlertifyService } from "../../../Services/alertify.service";
import { BreadcrumbService } from "../../../Services/breadcrumb.service";
import { BuildingService } from "../../../Services/building.service";
import { CityService } from "../../../Services/city.service";
import { CountryService } from "../../../Services/country.service";
import { DialogService } from "../../../Services/dialog.service";
import { DistrictService } from "../../../Services/district.service";
import { EmployeeService } from "../../../Services/employee.service";
import { TranslationService } from "../../../Services/translation.service";
import { Lookup } from "../../../Models/lookup";
import { forkJoin } from "rxjs";

@Component({
    selector: "app-building-add-update",
    templateUrl: "./building-add-update.component.html",
    styleUrls: ["./building-add-update.component.css"],
})
export class BuildingAddUpdateComponent implements OnInit {
    breadcrumbItems: Breadcrumb[] = [];
    buildingId: any = null;
    buildingForm!: FormGroup;
    submitted = false;
    countries: Lookup[] = [];
    cities: Lookup[] = [];
    districts: Lookup[] = [];
    employees: Lookup[] = [];

    building: Building = {
        employeeId: null,
        countryId: null,
        cityId: null,
        districtId: null,
        contributers: [],
    };
    status = "1";
    typeId = "";
    constructionStatusId = "";
    buildingTypes: Enum[] = [];
    buildingConstructionTypes: Enum[] = [];
    basicInfoCollapsed = false;
    addressCollapsed = false;
    contributersCollapsed = false;
    otherCollapsed = false;

    isCollapsed = false;
    showForm = false;
    @ViewChild("modal", { static: false }) modal?: ModalComponent;
    buildingContributer: BuildingContributer = {};

    constructor(
        private fb: FormBuilder,
        private cityService: CityService,
        private alertify: AlertifyService,
        private translateService: TranslationService,
        private countryService: CountryService,
        private districtService: DistrictService,
        private employeeService: EmployeeService,
        private buildingService: BuildingService,
        private route: ActivatedRoute,
        private router: Router,
        private breadcrumbService: BreadcrumbService,
        private dialogService: DialogService
    ) {
        this.buildingTypes = buildingService.BuildingTypes;
        this.buildingConstructionTypes = buildingService.ConstructionStatuses;
    }

    ngOnInit(): void {
        this.breadcrumbItems = this.breadcrumbService.BuildingDetailsItems;
        const id = this.route.snapshot.paramMap.get("id");
        if (id) {
            this.LoadBuildingWithLokkups(id);
        } else {
            this.LoadLookups();
        }
    }
    LoadBuildingWithLokkups(id: string) {
        forkJoin([
            this.buildingService.GetById(id),
            this.countryService.GetLookup(),
            this.employeeService.GetLookup(),
        ]).subscribe({
            next: ([buildingRes, countriesRes, employeesRes]) => {
                this.building = buildingRes;
                this.status = this.building.status === true ? "1" : "0";
                this.countries = countriesRes;
                this.employees = employeesRes;

                forkJoin([
                    this.cityService.GetLookup(this.building.countryId),
                    this.districtService.GetLookup(this.building.cityId),
                ]).subscribe({
                    next: ([citiesRes, districtsRes]) => {
                        this.cities = citiesRes;
                        this.districts = districtsRes;

                        this.CreateForm();
                    },
                    error: (error) => console.log(error),
                });
            },
            error: (error) => console.log(error),
        });
    }
    LoadLookups() {
        forkJoin([
            this.countryService.GetLookup(),
            this.employeeService.GetLookup(),
        ]).subscribe({
            next: ([countriesRes, employeesRes]) => {
                this.countries = countriesRes;
                this.employees = employeesRes;
                this.CreateForm();
            },
            error: (error) => console.log(error),
        });
    }
    CreateForm() {
        this.buildingForm = this.fb.group({
            BasicInfo: this.fb.group({
                SymbolNo: [null, Validators.required],
                Status: ["1"],
                Name: [null, Validators.required],
                EmployeeId: [
                    null,
                    [Validators.required, this.validateDropdown],
                ],
                BuildingTypeId: ["", Validators.required],
                BuildingConstructionStatusId: ["", Validators.required],
            }),
            AddressInfo: this.fb.group({
                CountryId: [null, [Validators.required, this.validateDropdown]],
                CityId: [null, [Validators.required, this.validateDropdown]],
                DistrictId: [
                    null,
                    [Validators.required, this.validateDropdown],
                ],
                AddressAR: [null],
                AddressEN: [null],
                Longitude: [null],
                Latitude: [null],
            }),
            OtherInfo: this.fb.group({
                EstablishYear: [null],
                TotalArea: [null],
                RentableArea: [null, this.smallerThan("TotalArea")],
                YearRentAmount: [null],
                YearReRentAmount: [null],
                LevelNo: [null],
                ReceiveDate: [null],
                Notes: [null],
            }),
        });
        this.OtherInfo?.get("TotalArea")?.valueChanges.subscribe(() =>
            this.OtherInfo?.get("RentableArea")?.updateValueAndValidity({
                onlySelf: true,
                emitEvent: false,
            })
        );
        this.showForm = true;
    }

    //#region Form Controllers
    get BasicInfo() {
        return this.buildingForm.controls["BasicInfo"] as FormGroup;
    }
    get AddressInfo() {
        return this.buildingForm.controls["AddressInfo"] as FormGroup;
    }
    get OtherInfo() {
        return this.buildingForm.controls["OtherInfo"] as FormGroup;
    }

    get SymbolNo() {
        return this.BasicInfo.controls["SymbolNo"] as FormControl;
    }
    get Name() {
        return this.BasicInfo.controls["Name"] as FormControl;
    }
    get EmployeeId() {
        return this.BasicInfo.controls["EmployeeId"] as FormControl;
    }
    get BuildingTypeId() {
        return this.BasicInfo.controls["BuildingTypeId"] as FormControl;
    }
    get BuildingConstructionStatusId() {
        return this.BasicInfo.controls[
            "BuildingConstructionStatusId"
        ] as FormControl;
    }

    get CountryId() {
        return this.AddressInfo.controls["CountryId"] as FormControl;
    }
    get CityId() {
        return this.AddressInfo.controls["CityId"] as FormControl;
    }
    get DistrictId() {
        return this.AddressInfo.controls["DistrictId"] as FormControl;
    }
    get AddressAR() {
        return this.AddressInfo.controls["AddressAR"] as FormControl;
    }
    get AddressEN() {
        return this.AddressInfo.controls["AddressEN"] as FormControl;
    }
    get TotalArea() {
        return this.OtherInfo.controls["TotalArea"] as FormControl;
    }
    get RentableArea() {
        return this.OtherInfo.controls["RentableArea"] as FormControl;
    }

    //#endregion

    toggleCollabse() {
        this.isCollapsed = !this.isCollapsed;
        this.basicInfoCollapsed =
            this.addressCollapsed =
            this.contributersCollapsed =
            this.otherCollapsed =
                this.isCollapsed;
    }

    get checkAllCollapsed() {
        this.isCollapsed =
            this.basicInfoCollapsed &&
            this.addressCollapsed &&
            this.contributersCollapsed &&
            this.otherCollapsed;
        return this.isCollapsed;
    }
    addBuildingContributer() {
        this.buildingContributer = {
            id: null,
            contributerId: null,
            tempId: null,
        };
        this.modal?.showModal();
    }
    editBuildingContributer(buildingContributer: BuildingContributer) {
        this.buildingContributer = { ...buildingContributer };
        this.modal?.showModal();
    }
    HideModal() {
        this.modal?.hideModal();
    }
    onSubmit() {
        this.submitted = true;
        if (this.buildingForm.invalid || this.hasContributers === false) {
            return;
        }
        this.building.status = this.status === "1" ? true : false;

        if (this.building.id === null) {
            this.Add();
        } else {
            this.Update();
        }
    }

    private Add() {
        this.building.id = uuidv4();
        for (const contributer of this.building.contributers || []) {
            contributer.id = uuidv4();
        }
        this.buildingService.CreateNewBuilding(this.building).subscribe(
            () => {
                const successMsg =
                    this.translateService.Translate("AddedSuccessfully");
                this.alertify.success(successMsg);
                this.backToList();
            },
            (error) => {
                console.log(error);
                this.building.id = null;
            }
        );
    }
    private Update() {
        for (const contributer of this.building.contributers || []) {
            if (contributer.id === null) {
                contributer.id = uuidv4();
            }
        }
        this.buildingService
            .EditBuilding(this.building.id, this.building)
            .subscribe(
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
        this.buildingForm.reset();
        this.submitted = false;
        this.status = "1";
    }

    backToList() {
        this.router.navigate(["/definitions/buildings"]);
    }

    get hasContributers() {
        const len = this.building.contributers?.length || 0;
        return len > 0;
    }

    addUpdateContributer(data: BuildingContributer) {
        console.log(data);
        this.building.contributers = this.building.contributers || [];
        const isExist =
            this.building.contributers.find(
                (x) =>
                    (x.id != data.id || x.tempId != data.tempId) &&
                    x.contributerId === data.contributerId
            ) != null;
        if (isExist) {
            this.alertify.warning("Exist before");
            return;
        }
        this.HideModal();

        if (data.id != null) {
            const index = this.building.contributers.findIndex(
                (x) => x.id == data.id
            );
            if (index > -1) {
                this.building.contributers[index] = { ...data };
            }
        } else {
            const index = this.building.contributers.findIndex(
                (x) => x.tempId == data.tempId
            );
            if (index === -1) {
                this.building.contributers.push({ ...data });
            } else {
                this.building.contributers[index] = { ...data };
            }
        }
    }

    deleteBuildingContributer(buildingContributer: BuildingContributer) {
        const deleteMsg = this.translateService.Translate("DeleteMessage");
        this.dialogService.confirm(deleteMsg).subscribe((res) => {
            if (res) {
                this.building.contributers =
                    this.building?.contributers?.filter(
                        (x) =>
                            x.contributerId !==
                            buildingContributer.contributerId
                    );
            }
        });
    }
    getCitiesByCountryId(countryId: string) {
        if (countryId) {
            this.cityService.GetLookup(countryId).subscribe({
                next: (cities) => (this.cities = cities),
                error: (error) => console.log(error),
            });
        } else {
            this.building.cityId = null;
            this.building.districtId = null;
            this.cities = [];
            this.districts = [];
        }
    }
    getDistrictsByCityId(cityId: string) {
        if (cityId) {
            this.districtService.GetLookup(cityId).subscribe({
                next: (districts) => (this.districts = districts),
                error: (error) => console.log(error),
            });
        } else {
            this.building.districtId = null;
            this.districts = [];
        }
    }
    //#region Validations :
    // Only Integer Numbers
    keyPressNumbers(event: any) {
        const charCode = event.which ? event.which : event.keyCode;
        // Only Numbers 0-9
        if (charCode < 48 || charCode > 57) {
            event.preventDefault();
            return false;
        } else {
            return true;
        }
    }

    keyPressNumbersWithDecimal(event: any, value: any) {
        const charCode = event.which ? event.which : event.keyCode;
        if (charCode === 46 && value?.indexOf(event.key) > -1) {
            return false;
        }
        if (
            charCode !== 46 &&
            charCode > 31 &&
            (charCode < 48 || charCode > 57)
        ) {
            event.preventDefault();
            return false;
        }
        return true;
    }
    smallerThan(otherControlName: string) {
        return (
            control: AbstractControl
        ): { [key: string]: boolean } | null => {
            if (!control.parent) {
                return null; // Control is not yet associated with a parent.
            }
            const thisValue = control.value;
            const otherValue = control?.parent?.get(otherControlName)?.value;

            if (
                thisValue == undefined ||
                thisValue == null ||
                thisValue == "" ||
                thisValue == 0
            ) {
                return null;
            }
            if (+thisValue <= +otherValue) {
                return null;
            }

            return {
                smallerthan: true,
            };
        };
    }

    greaterThan(otherControlName: string) {
        return (
            control: AbstractControl
        ): { [key: string]: boolean } | null => {
            if (!control.parent) {
                return null; // Control is not yet associated with a parent.
            }
            const thisValue = control.value;
            const otherValue = control?.parent?.get(otherControlName)?.value;

            if (
                thisValue == undefined ||
                thisValue == null ||
                thisValue == "" ||
                thisValue == 0
            ) {
                return null;
            }
            if (+thisValue >= +otherValue) {
                return null;
            }

            return {
                greaterthan: true,
            };
        };
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
    //#endregion
}
