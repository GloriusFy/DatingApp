import { Component, ChangeDetectionStrategy, Input, Output, EventEmitter } from '@angular/core';
import { Photo } from 'src/app/core/models/member.model';

@Component({
    selector: 'da-photo-editor',
    templateUrl: './photo-editor.component.html',
    styleUrls: ['./photo-editor.component.css'],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class PhotoEditorComponent {
    @Input()
    public photos: Photo[] = [];

    @Input()
    public uploadingProgress: number | null = null;

    @Output()
    public mainPhotoChange = new EventEmitter<Photo>();

    @Output()
    public newPhotoUpload = new EventEmitter<File>();

    @Output()
    public photoDelete = new EventEmitter<Photo>();

    public onSetMainPhotoClick(photo: Photo) {
        this.mainPhotoChange.emit(photo);
    }

    public onPhotoDeleteClick(photo: Photo) {
        this.photoDelete.emit(photo);
    }

    public onFileInputChange(e: Event) {
        this.uploadingProgress = 0;

        const fileList = (e.target as HTMLInputElement).files;
        const file = fileList![0];

        this.newPhotoUpload.emit(file);
    }
}
