import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Member, Photo } from '../core/models/member.model';
import { CurrentMemberApi } from './api/current-member.api';
import { UpdateMemberInfoRequest } from './models/current-member.model';
import { CurrentMemberState } from './state/current-member.state';

@Injectable()
export class CurrentMemberFacade {
    constructor(
        private readonly currentMemberApi: CurrentMemberApi,
        private readonly currentMemberState: CurrentMemberState)
    {
    }

    public getCurrentMember(): Observable<Member> {
        return this.currentMemberState.getCurrentMember();
    }

    public getPhotoUploadingProgress(): Observable<number | null> {
        return this.currentMemberApi.getPhotoUploadingProgress();
    }

    public loadCurrentMember() {
        return this.currentMemberApi.get()
            .subscribe(member => {
                this.currentMemberState.setCurrentMember(member);
            });
    }

    public updateCurentMemberInfo(request: UpdateMemberInfoRequest): Observable<void> {
        return this.currentMemberApi.updateMemberInfo(request);
    }

    public uploadNewPhoto(photo: File) {
        this.currentMemberApi.uploadPhoto(photo)
            .subscribe(uploadedPhoto => {
                if (uploadedPhoto){
                    this.currentMemberState.addPhoto(uploadedPhoto);
                }
            })
    }

    public setMainPhoto(photo: Photo) {
        this.currentMemberApi.setMainPhoto(photo.name)
            .subscribe(() => {
                this.currentMemberState.setPhotoAsMain(photo);
            })
    }

    public deletePhoto(photo: Photo) {
        this.currentMemberApi.deletePhoto(photo.name)
            .subscribe(() => {
                this.currentMemberState.deletePhoto(photo.name);
            })
    }
}