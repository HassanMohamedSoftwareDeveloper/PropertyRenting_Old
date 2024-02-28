import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  OnChanges,
} from "@angular/core";
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from "@angular/forms";
import { AlertifyService } from "../../../Services/alertify.service";
import { CityService } from "../../../Services/city.service";
import { TranslationService } from "../../../Services/translation.service";
import { v4 as uuidv4 } from "uuid";
import { CountryService } from "../../../Services/country.service";
import { DistrictService } from "../../../Services/district.service";
import { District } from "../../../Models/district";
import { Lookup } from "../../../Models/lookup";

@Component({
  selector: "app-district-add-update",
  templateUrl: "./district-add-update.component.html",
  styleUrls: ["./district-add-update.component.css"],
})
export class DistrictAddUpdateComponent implements OnInit, OnChanges {
  districtForm!: FormGroup;
  @Input() district: District = {
    id: null,
    nameAR: "",
    nameEN: "",
    countryId: null,
    country: "",
    city: "",
    cityId: null,
  };
  @Output() hideModalWithRefreshEvent = new EventEmitter<boolean>();
  submitted = false;
  showForm = false;
  countries: Lookup[] = [];
  cities: Lookup[] = [];
  constructor(
    private fb: FormBuilder,
    private cityService: CityService,
    private alertify: AlertifyService,
    private translateService: TranslationService,
    private countryService: CountryService,
    private districtService: DistrictService
  ) { }

  ngOnInit(): void {
    this.LoadCountries();
  }

  ngOnChanges() {
    if (this.district.id) {
      this.getCitiesByCountryId(this.district.countryId);
    } else {
      this.cities = [];
    }
  }
  LoadCountries() {
    this.countryService.GetLookup().subscribe({
      next: (countriesRes) => {
        this.countries = countriesRes;
        this.CreateFrom();
      },
      error: (error) => console.log(error),
    });
  }

  CreateFrom() {
    this.districtForm = this.fb.group({
      NameAR: [null, Validators.required],
      NameEN: [null],
      CountryId: [null, [Validators.required, this.validateDropdown]],
      CityId: [null, [Validators.required, this.validateDropdown]],
    });
    this.showForm = true;
  }

  //#region Form Controllers
  get NameAR() {
    return this.districtForm.controls["NameAR"] as FormControl;
  }
  get NameEN() {
    return this.districtForm.controls["NameEN"] as FormControl;
  }
  get CountryId() {
    return this.districtForm.controls["CountryId"] as FormControl;
  }
  get CityId() {
    return this.districtForm.controls["CityId"] as FormControl;
  }
  //#endregion

  onSubmit() {
    this.submitted = true;
    if (this.districtForm.invalid) {
      return;
    }
    if (this.district.id == null) {
      this.Add();
    } else {
      this.Update();
    }
  }

  private Add() {
    this.district.id = uuidv4();
    this.districtService.CreateNewDistrict(this.district).subscribe(
      () => {
        const successMsg =
          this.translateService.Translate("AddedSuccessfully");
        this.alertify.success(successMsg);
        this.cancel(true);
      },
      (error) => {
        console.log(error);
        this.district.id = null;
      }
    );
  }
  private Update() {
    this.districtService
      .EditDistrict(this.district.id, this.district)
      .subscribe(
        () => {
          const successMsg = this.translateService.Translate(
            "UpdatedSuccessfully"
          );
          this.alertify.success(successMsg);
          this.cancel(true);
        },
        (error) => console.log(error)
      );
  }

  getCitiesByCountryId(countryId: string) {
    this.cityService.GetLookup(countryId).subscribe({
      next: (citiesRes) => (this.cities = citiesRes),
      error: (error) => console.log(error),
    });
  }

  resetFrom() {
    this.districtForm.reset();
    this.submitted = false;
  }

  cancel(isRefresh: boolean) {
    this.resetFrom();
    this.hideModalWithRefreshEvent.emit(isRefresh);
  }

  validateDropdown(control: AbstractControl) {
    const thisValue = control.value;
    if (
      thisValue === undefined ||
      thisValue === null ||
      thisValue === "" ||
      thisValue === "null"
    ) {
      return { required: true };
    }
    return null;
  }
}
