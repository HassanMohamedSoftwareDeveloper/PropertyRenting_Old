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
import { v4 as uuidv4 } from "uuid";
import { BuildingContributer } from "../../../Models/building-contributer";
import { ContributerService } from "../../../Services/contributer.service";
import { Lookup } from "../../../Models/lookup";

@Component({
    selector: "app-building-contributer-add-update",
    templateUrl: "./building-contributer-add-update.component.html",
    styleUrls: ["./building-contributer-add-update.component.css"],
})
export class BuildingContributerAddUpdateComponent
    implements OnInit, OnChanges
{
    buildingContributerForm!: FormGroup;
    @Input() buildingContributer: BuildingContributer = {
        contributerId: null,
    };
    @Output() hideModalEvent = new EventEmitter<void>();
    @Output() addedContributerEvent = new EventEmitter<BuildingContributer>();
    submitted = false;

    contributers: Lookup[] = [];
    constructor(
        private fb: FormBuilder,
        private contributerService: ContributerService
    ) {}

    ngOnInit(): void {
        this.CreateFrom();
        this.loadContributers();
    }
    ngOnChanges(): void {
        if (
            this.buildingContributer.id === null &&
            this.buildingContributer.contributerId === null
        ) {
            this.resetFrom();
        }
    }
    loadContributers() {
        this.contributerService.GetLookup().subscribe({
            next: (res) => (this.contributers = res),
            error: (error) => console.log(error),
        });
    }
    CreateFrom() {
        this.buildingContributerForm = this.fb.group({
            ContributerId: [null, [Validators.required, this.validateDropdown]],
            Percentage: [null, [Validators.required, Validators.max(100)]],
        });
    }

    //#region Form Controllers
    get ContributerId() {
        return this.buildingContributerForm.controls[
            "ContributerId"
        ] as FormControl;
    }
    get Percentage() {
        return this.buildingContributerForm.controls[
            "Percentage"
        ] as FormControl;
    }
    //#endregion

    onSubmit() {
        this.submitted = true;
        if (this.buildingContributerForm.invalid) {
            return;
        }
        const contributer = this.contributers.find(
            (x) => x.id === this.buildingContributer.contributerId
        );
        this.buildingContributer.contributer = contributer?.description;
        if (
            this.buildingContributer.id === null &&
            this.buildingContributer.tempId === null
        ) {
            this.buildingContributer.tempId = uuidv4();
        }
        this.addedContributerEvent.emit(this.buildingContributer);
    }

    resetFrom() {
        this.buildingContributerForm?.reset();
        this.submitted = false;
    }

    cancel() {
        this.resetFrom();
        this.hideModalEvent.emit();
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
