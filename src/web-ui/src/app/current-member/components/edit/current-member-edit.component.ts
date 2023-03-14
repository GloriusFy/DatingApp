import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { FormDeactivatableComponent } from 'src/app/core/components/form-deactivatable.component';
import { Member, Photo } from 'src/app/core/models/member.model';
import { NotificationService } from 'src/app/core/services/notification/notification.service';
import { CurrentMemberFacade } from '../../current-member.facade';
import { UpdateMemberInfoRequest } from '../../models/current-member.model';

@Component({
    selector: 'da-current-member-edit',
    templateUrl: './current-member-edit.component.html',
    styleUrls: ['./current-member-edit.component.css'],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class CurrentMemberEditComponent extends FormDeactivatableComponent implements OnInit {
    public member$: Observable<Member>;
    public photoUploadingProgress$: Observable<number | null>;
    public memberForm: FormGroup;

    constructor(
        private readonly currentMemberFacade: CurrentMemberFacade,
        private readonly notificationService: NotificationService,
        private readonly formBuilder: FormBuilder
    ) {
        super();
        this.member$ = this.currentMemberFacade.getCurrentMember()
            .pipe(
                tap(m => {
                    if (!m)
                        return;

                    this.form.patchValue({
                        description: m.briefDescription,
                        lookingFor: m.lookingFor,
                        interests: m.interests,
                        locationDetails: {
                            city: m.city,
                            country: m.country
                        }
                    })
                })
            );
        this.photoUploadingProgress$ = this.currentMemberFacade.getPhotoUploadingProgress();
        this.memberForm = this.createForm();
    }

    public get form(): FormGroup {
        return this.memberForm;
    }

    public ngOnInit(): void {
        this.currentMemberFacade.loadCurrentMember();
    }

    public onSubmit(): void {
        const updateRequest = this.formValueToUpdateRequest();

        this.currentMemberFacade.updateCurentMemberInfo(updateRequest)
            .subscribe(() => {
                this.notificationService.showSuccess("Submitted!");
                this.form.reset(this.form.value);
            });
    }

    public getMainPhotoUrl(member: Member): string | undefined {
        return member.photos.find(p => p.isMain)?.url;
    }

    public onNewPhotoUpload(photo: File) {
        this.currentMemberFacade.uploadNewPhoto(photo);
    }

    public onMainPhotoChange(photo: Photo) {
        this.currentMemberFacade.setMainPhoto(photo);
    }

    public onPhotoDelete(photo: Photo) {
        this.currentMemberFacade.deletePhoto(photo);
    }

    private formValueToUpdateRequest(): UpdateMemberInfoRequest {
        return {
            briefDescription: this.memberForm.get('description')?.value,
            lookingFor: this.memberForm.get('lookingFor')?.value,
            interests: this.memberForm.get('interests')?.value,
            city: this.memberForm.get('locationDetails.city')?.value,
            country: this.memberForm.get('locationDetails.country')?.value
        };
    }

    private createForm(): FormGroup {
        return this.formBuilder.group({
            description: this.formBuilder.control(''),
            lookingFor: this.formBuilder.control(''),
            interests: this.formBuilder.control(''),
            locationDetails: this.formBuilder.group({
                city: this.formBuilder.control(''),
                country: this.formBuilder.control('')
            })
        });
    }
}
