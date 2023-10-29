import {
    Component,
    EventEmitter,
    Input,
    OnInit,
    Output,
    OnChanges,
} from "@angular/core";
import {
    FormBuilder,
    FormControl,
    FormGroup,
    Validators,
} from "@angular/forms";
import { v4 as uuidv4 } from "uuid";
import { ContactPerson } from "../../../Models/contact-person";
import { Enum } from "../../../Models/enum";
import { RenterService } from "../../../Services/renter.service";
import { TranslationService } from "../../../Services/translation.service";

@Component({
    selector: "app-renter-contact-person-add-update",
    templateUrl: "./renter-contact-person-add-update.component.html",
    styleUrls: ["./renter-contact-person-add-update.component.css"],
})
export class RenterContactPersonAddUpdateComponent
    implements OnInit, OnChanges
{
    contactPersonForm!: FormGroup;
    @Input() contactPerson: ContactPerson = { tempId: null };
    @Output() hideModalEvent = new EventEmitter<void>();
    @Output() addedContactPersonEvent = new EventEmitter<ContactPerson>();
    submitted = false;
    status = "1";
    identityTypes: Enum[] = [];

    constructor(
        private fb: FormBuilder,
        private translateService: TranslationService,
        private renterService: RenterService
    ) {
        this.identityTypes = renterService.IdentityTypes;
    }

    ngOnInit(): void {
        this.createForm();
    }
    ngOnChanges(): void {
        if (
            this.contactPerson.id == null &&
            this.contactPerson.tempId == null
        ) {
            this.resetFrom();
        } else {
            this.status = this.contactPerson.status === true ? "1" : "0";
        }
    }
    createForm() {
        this.contactPersonForm = this.fb.group({
            Status: ["1", Validators.required],
            Code: [null, Validators.required],
            NameAR: [null, Validators.required],
            NameEN: [null],
            PhoneNumber: [null],
            MobileNumber: [null],
            Email: [null, Validators.email],
            IdentityType: [null],
            IdentityNumber: [null],
            IdentityIssuePlace: [null],
            IdentityIssueDate: [null],
            IdentityExpiryDate: [null],
            Notes: [null],
        });
    }

    get Status() {
        return this.contactPersonForm.controls["Status"] as FormControl;
    }
    get Code() {
        return this.contactPersonForm.controls["Code"] as FormControl;
    }
    get NameAR() {
        return this.contactPersonForm.controls["NameAR"] as FormControl;
    }
    get NameEN() {
        return this.contactPersonForm.controls["NameEN"] as FormControl;
    }
    get Email() {
        return this.contactPersonForm.controls["Email"] as FormControl;
    }

    onSubmit() {
        this.submitted = true;
        if (this.contactPersonForm.invalid) {
            return;
        }
        if (
            this.contactPerson.id == null &&
            this.contactPerson.tempId == null
        ) {
            this.contactPerson.tempId = uuidv4();
        }
        this.contactPerson.status = this.status === "1" ? true : false;
        this.addedContactPersonEvent.emit(this.contactPerson);
    }

    resetFrom() {
        this.contactPersonForm?.reset();
        this.submitted = false;
        this.status = "1";
    }

    cancel() {
        this.resetFrom();
        this.hideModalEvent.emit();
    }
}
