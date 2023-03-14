import { Gender, MemberSummary } from 'src/app/core/models/member.model';

export interface MemberFilter {
    gender?: Gender,
    minAge?: number,
    maxAge?: number,
    sortBy: SortableField
}

export interface MemberSummaryWithStatus extends MemberSummary {
    online: boolean;
}

export const enum SortableField {
    LastActive = "LastActive",
    Created = "Created"
}