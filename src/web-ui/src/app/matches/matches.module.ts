import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatchesRoutingModule } from './matches-routing.module';
import { MatchListComponent } from './match-list/components/list/match-list.component';
import { MatchListFacade } from './match-list/match-list.facade';
import { MatchListApi } from './match-list/api/match-list.api';
import { LikeCardComponent } from './match-list/components/like-card/like-card.component';
import { SharedModule } from '../shared/shared.module';

@NgModule({
    declarations: [
        MatchListComponent,
        LikeCardComponent
    ],
    imports: [
        CommonModule,
        MatchesRoutingModule,
        MatButtonToggleModule,
        MatButtonModule,
        MatIconModule,
        SharedModule
    ],
    providers: [
        MatchListFacade,
        MatchListApi
    ]
})
export class MatchesModule { }
