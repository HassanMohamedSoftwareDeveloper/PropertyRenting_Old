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
import { City } from "../../../Models/city";
import { Country } from "../../../Models/country";
import { District } from "../../../Models/district";
import { Employee } from "../../../Models/employee";
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
    countries: Country[] = [];
    cities: City[] = [];
    citiesList: City[] = [];
    districts: District[] = [];
    districtsList: District[] = [];
    employees: Employee[] = [];

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
        this.CreateFrom();
        this.LoadCountries();

        this.LoadEmployees();

        this.LoadCities();
        this.LoadDistricts();

        this.breadcrumbItems = this.breadcrumbService.BuildingDetailsItems;
        const id = this.route.snapshot.paramMap.get("id");
        if (id) {
            this.getBuildingById(id);
        }

        this.OtherInfo?.get("TotalArea")?.valueChanges.subscribe(() =>
            this.OtherInfo?.get("RentableArea")?.updateValueAndValidity({
                onlySelf: true,
                emitEvent: false,
            })
        );
    }

    getBuildingById(id: string) {
        this.buildingService.GetById(id).subscribe(
            (result) => {
                this.building = result;
                this.status = this.building.status === true ? "1" : "0";
                this.getCitiesByCountryId(this.building.countryId);
                this.getDistrictsByCityId(this.building.cityId);
            },
            (error) => console.log(error)
        );
    }
    LoadCountries() {
        this.countryService.GetAllCountries().subscribe(
            (result) => {
                this.countries = result;
            },
            (error) => console.log(error)
        );
    }
    LoadCities() {
        this.cityService.GetAllCities().subscribe(
            (result) => {
                this.citiesList = result;
                this.getCitiesByCountryId(this.building.countryId);
            },
            (error) => console.log(error)
        );
    }
    LoadDistricts() {
        this.districtService.GetAllDistricts().subscribe(
            (result) => {
                this.districtsList = result;
                this.getDistrictsByCityId(this.building.cityId);
            },
            (error) => console.log(error)
        );
    }
    LoadEmployees() {
        this.employeeService.GetAllEmployees().subscribe(
            (res) => (this.employees = res),
            (error) => console.log(error)
        );
    }

    CreateFrom() {
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
                AddressAR: [null, Validators.required],
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
        this.buildingContributer = { id: null, contributerId: null };
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
        //this.building.typeId = +this.typeId;
        //this.building.constructionStatusId = +this.constructionStatusId;

        if (this.building.id == null) {
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

    getCitiesByCountryId(countryId: any) {
        this.cities = this.citiesList.filter((c) => c.countryId === countryId);
        this.getDistrictsByCityId(null);
    }

    getDistrictsByCityId(cityId: any) {
        this.districts = this.districtsList.filter((c) => c.cityId === cityId);
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
