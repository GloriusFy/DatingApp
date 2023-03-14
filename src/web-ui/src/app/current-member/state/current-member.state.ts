import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Member, Photo } from 'src/app/core/models/member.model';
import { UserService } from 'src/app/core/services/user/user.service';

@Injectable()
export class CurrentMemberState {
    private readonly currentMember$ = new BehaviorSubject<Member>(null!);

    constructor(private readonly userService: UserService){
    }

    public getCurrentMember(): Observable<Member> {
        return this.currentMember$.asObservable();
    }

    public setCurrentMember(member: Member) {
        this.currentMember$.next(member);
    }

    public addPhoto(photo: Photo) {
        const currentMember = this.currentMember$.value;

        if (currentMember.photos.length === 0) {
            photo.isMain = true;
            this.userService.changeMainPhoto(photo.url);
        }

        currentMember.photos.push(photo);

        this.currentMember$.next(currentMember);
    }

    public setPhotoAsMain(photo: Photo) {
        const currentMember = this.currentMember$.value;

        const currentMainPhoto = currentMember.photos.find(ph => ph.isMain);
        currentMainPhoto!.isMain = false;

        const newMainPhoto = currentMember.photos.find(ph => ph.name === photo.name);
        newMainPhoto!.isMain = true;

        currentMember.photos = [...currentMember.photos];

        this.currentMember$.next(currentMember);
        this.userService.changeMainPhoto(photo.url);
    }

    public deletePhoto(photoName: string) {
        const currentMember = this.currentMember$.value;

        currentMember.photos = currentMember.photos.filter(p => p.name !== photoName);

        this.currentMember$.next(currentMember);
    }
}