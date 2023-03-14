import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CurrentMemberEditComponent } from './components/edit/current-member-edit.component';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatListModule } from '@angular/material/list';
import { MatTabsModule } from '@angular/material/tabs';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { ReactiveFormsModule } from '@angular/forms';
import { CurrentMemberFacade } from './current-member.facade';
import { MatIconModule } from '@angular/material/icon';
import { PhotoEditorComponent } from './components/edit/photo-editor/photo-editor.component';
import { CurrentMemberRoutingModule } from './current-member.routing.module';
import { CurrentMemberState } from './state/current-member.state';
import { SharedModule } from '../shared/shared.module';
import { CurrentMemberApi } from './api/current-member.api';

@NgModule({
    declarations: [
        CurrentMemberEditComponent,
        PhotoEditorComponent
    ],
    imports: [
        CommonModule,
        MatCardModule,
        MatButtonModule,
        MatListModule,
        MatTabsModule,
        MatFormFieldModule,
        MatInputModule,
        MatProgressBarModule,
        ReactiveFormsModule,
        MatIconModule,
        CurrentMemberRoutingModule,
        SharedModule
    ],
    providers: [
        CurrentMemberFacade,
        CurrentMemberState,
        CurrentMemberApi
    ]
})
export class CurrentMemberModule { }
