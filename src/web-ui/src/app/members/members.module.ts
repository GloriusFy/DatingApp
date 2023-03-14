import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MemberDetailsComponent } from './member-details/member-details.component';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RouterModule } from '@angular/router';
import { MemberService } from './services/member.service';
import { MatListModule } from '@angular/material/list';
import { MatTabsModule } from '@angular/material/tabs';
import { SharedModule } from '../shared/shared.module';
import { MemberResolver } from './services/member.resolver';
import { MembersRoutingModule } from './members-routing.module';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSelectModule } from '@angular/material/select';
import { ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatInputModule } from '@angular/material/input';
import { MemberListComponent } from './member-list/components/list/member-list.component';
import { MemberCardComponent } from './member-list/components/card/member-card.component';
import { MemberFilterComponent } from './member-list/components/filter/member-filter.component';
import { MemberListFacade } from './member-list/member-list.facade';
import { MemberListState } from './member-list/state/member-list.state';
import { MemberListApi } from './member-list/api/member-list.api';
import { MemberMessagesApi } from './member-messages/api/member-messages.api';
import { MemberMessagesFacade } from './member-messages/member-messages.facade';
import { MemberMessagesState } from './member-messages/state/member-messages.state';
import { MemberMessagesComponent } from './member-messages/components/messages/member-messages.component';

@NgModule({
    declarations: [
        MemberListComponent,
        MemberDetailsComponent,
        MemberCardComponent,
        MemberFilterComponent,
        MemberMessagesComponent
    ],
    imports: [
        CommonModule,
        MatCardModule,
        MatButtonModule,
        MatIconModule,
        MatListModule,
        MatTabsModule,
        SharedModule,
        MembersRoutingModule,
        MatPaginatorModule,
        MatSelectModule,
        MatInputModule,
        MatFormFieldModule,
        MatButtonToggleModule,
        ReactiveFormsModule,
        RouterModule
    ],
    providers: [
        MemberService,
        MemberResolver,
        MemberListFacade,
        MemberListState,
        MemberListApi,
        MemberMessagesApi,
        MemberMessagesFacade,
        MemberMessagesState
    ]
})
export class MembersModule { }
