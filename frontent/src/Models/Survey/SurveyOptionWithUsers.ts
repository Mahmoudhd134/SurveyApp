import { SurveyOptionModel } from "./SurveyOptionModel";

export interface SurveyOptionWithUsers extends SurveyOptionModel {
    userIds: string[];
    usernames: string[];
}