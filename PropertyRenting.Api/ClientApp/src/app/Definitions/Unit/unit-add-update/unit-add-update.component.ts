import { Component, OnInit } from "@angular/core";
import {
    AbstractControl,
    FormBuilder,
    FormControl,
    FormGroup,
    Validators,
} from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { v4 as uuidv4 } from "uuid";
import { Breadcrumb } from "../../../Models/breadcrumb";
import { Unit } from "../../../Models/unit";
import { AlertifyService } from "../../../Services/alertify.service";
import { BreadcrumbService } from "../../../Services/breadcrumb.service";
import { BuildingService } from "../../../Services/building.service";
import { CityService } from "../../../Services/city.service";
import { CountryService } from "../../../Services/country.service";
import { DistrictService } from "../../../Services/district.service";
import { TranslationService } from "../../../Services/translation.service";
import { UnitService } from "../../../Services/unit.service";
import { Lookup } from "../../../Models/lookup";
import { forkJoin } from "rxjs";

@Component({
    selector: "app-unit-add-update",
    templateUrl: "./unit-add-update.component.html",
    styleUrls: ["./unit-add-update.component.css"],
})
export class UnitAddUpdateComponent implements OnInit {
    breadcrumbItems: Breadcrumb[] = [];
    //#region Local Vars
    unitId: any = null;
    submitted = false;
    status = "1";
    hasCentralAC = "0";
    hasInternetService = "0";
    unit: Unit = {
        countryId: null,
        cityId: null,
        districtId: null,
    };

    unitForm!: FormGroup;
    showForm = false;
    //#region Collapsed
    basicInfoCollapsed = false;
    addressCollapsed = false;
    otherCollapsed = false;
    isCollapsed = false;
    //#endregion
    //#endregion

    //#region Lists
    buildings: Lookup[] = [];
    countries: Lookup[] = [];
    cities: Lookup[] = [];
    districts: Lookup[] = [];
    //#endregion

    //#region CTOR
    constructor(
        private fb: FormBuilder,
        private cityService: CityService,
        private alertify: AlertifyService,
        private translateService: TranslationService,
        private countryService: CountryService,
        private districtService: DistrictService,
        private buildingService: BuildingService,
        private unitService: UnitService,
        private route: ActivatedRoute,
        private router: Router,
        private breadcrumbService: BreadcrumbService
    ) {}
    //#endregion

    //#region Methods Implmentation
    ngOnInit(): void {
        this.breadcrumbItems = this.breadcrumbService.UnitDetailsItems;
        const id = this.route.snapshot.paramMap.get("id");
        if (id) {
            this.LoadUnitWithLokkups(id);
        } else {
            this.LoadLookups();
        }
    }
    LoadUnitWithLokkups(id: string) {
        forkJoin([
            this.unitService.GetById(id),
            this.buildingService.GetLookup(),
            this.countryService.GetLookup(),
        ]).subscribe({
            next: ([unitRes, buildingsRes, countriesRes]) => {
                this.unit = unitRes;
                this.status = this.unit.status === true ? "1" : "0";
                this.hasCentralAC = this.unit.hasCentralAC === true ? "1" : "0";
                this.hasInternetService =
                    this.unit.hasInternetService === true ? "1" : "0";
                this.buildings = buildingsRes;
                this.countries = countriesRes;

                forkJoin([
                    this.cityService.GetLookup(this.unit.countryId),
                    this.districtService.GetLookup(this.unit.cityId),
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
            this.buildingService.GetLookup(),
            this.countryService.GetLookup(),
        ]).subscribe({
            next: ([buildingRes, countriesRes]) => {
                this.buildings = buildingRes;
                this.countries = countriesRes;
                this.CreateForm();
            },
            error: (error) => console.log(error),
        });
    }
    //#endregion

    //#region Methods

    //#region Operation Methods :
    CreateForm() {
        this.unitForm = this.fb.group({
            BasicInfo: this.fb.group({
                Status: ["1"],
                UnitNumber: [null, Validators.required],
                UnitName: [null, Validators.required],
                BuildingId: [
                    null,
                    [Validators.required, this.validateDropdown],
                ],
                Floor: [null, Validators.required],
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
                TotalArea: [null],
                RentableArea: [null, this.smallerThan("TotalArea")],
                AnnualRentAmount: [null],
                ReceiveDate: [null],
                HasCentralAC: ["0"],
                HasInternetService: ["0"],
                Notes: [null],
                RoomsNumber: [null],
                PathsNumber: [null],
                HallNumber: [null],
                ACNumber: [null],
                KitchenNumber: [null],
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
    onSubmit() {
        this.submitted = true;
        if (this.unitForm.invalid) {
            return;
        }
        this.unit.status = this.status === "1" ? true : false;
        this.unit.hasCentralAC = this.hasCentralAC === "1" ? true : false;
        this.unit.hasInternetService =
            this.hasInternetService === "1" ? true : false;

        if (this.unit.id == null) {
            this.Add();
        } else {
            this.Update();
        }
    }
    private Add() {
        this.unit.id = uuidv4();
        this.unitService.Create(this.unit).subscribe(
            () => {
                const successMsg =
                    this.translateService.Translate("AddedSuccessfully");
                this.alertify.success(successMsg);
                this.backToList();
            },
            (error) => {
                console.log(error);
                this.unit.id = null;
            }
        );
    }
    private Update() {
        this.unitService.Edit(this.unit.id, this.unit).subscribe(
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
        this.unitForm.reset();
        this.submitted = false;
        this.status = "1";
        this.hasCentralAC = "0";
        this.hasInternetService = "0";
    }
    backToList() {
        this.router.navigate(["/definitions/units"]);
    }
    toggleCollabse() {
        this.isCollapsed = !this.isCollapsed;
        this.basicInfoCollapsed =
            this.addressCollapsed =
            this.otherCollapsed =
                this.isCollapsed;
    }
    setAddressInfoAccordingToChangeBuilding(buildingId: any) {
        this.buildingService.GetById(buildingId).subscribe({
            next: (building) => {
                forkJoin([
                    this.cityService.GetLookup(building.countryId),
                    this.districtService.GetLookup(building.cityId),
                ]).subscribe({
                    next: ([cities, districts]) => {
                        this.cities = cities;
                        this.districts = districts;

                        this.unit.countryId = building?.countryId;
                        this.unit.cityId = building?.cityId;
                        this.unit.districtId = building?.districtId;
                        this.unit.addressAR = building?.addressAR;
                        this.unit.addressEN = building?.addressEN;
                        this.unit.longitude = building?.longtude;
                        this.unit.latitude = building?.latitude;
                    },
                    error: (error) => console.log(error),
                });
            },
            error: (error) => console.log(error),
        });
    }
    //#endregion

    //#region Filter Lists
    getCitiesByCountryId(countryId: string) {
        if (countryId) {
            this.cityService.GetLookup(countryId).subscribe({
                next: (cities) => (this.cities = cities),
                error: (error) => console.log(error),
            });
        } else {
            this.unit.cityId = null;
            this.unit.districtId = null;
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
            this.unit.districtId = null;
            this.districts = [];
        }
    }
    //#endregion
    //#endregion

    //#region getters
    get checkAllCollapsed() {
        this.isCollapsed =
            this.basicInfoCollapsed &&
            this.addressCollapsed &&
            this.otherCollapsed;
        return this.isCollapsed;
    }
    //#region form groups
    get BasicInfo() {
        return this.unitForm.controls["BasicInfo"] as FormGroup;
    }
    get AddressInfo() {
        return this.unitForm.controls["AddressInfo"] as FormGroup;
    }
    get OtherInfo() {
        return this.unitForm.controls["OtherInfo"] as FormGroup;
    }
    //#endregion
    //#region controlers
    //#region Basic Info
    get UnitNumber() {
        return this.BasicInfo.controls["UnitNumber"] as FormControl;
    }
    get UnitName() {
        return this.BasicInfo.controls["UnitName"] as FormControl;
    }
    get BuildingId() {
        return this.BasicInfo.controls["BuildingId"] as FormControl;
    }
    get Floor() {
        return this.BasicInfo.controls["Floor"] as FormControl;
    }
    //#endregion
    //#region Address Info
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
    //#endregion
    //#region Other Info
    get RentableArea() {
        return this.OtherInfo.controls["RentableArea"] as FormControl;
    }
    //#endregion
    //#endregion
    //#endregion

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
