import { SurveyOptionModel } from "./SurveyOptionModel";

export interface SurveyForPageModel {
    id: number;
    question: string;
    isAnswered: boolean;
    answerId: number;
    totalAnswers: number;
    optionDtos: SurveyOptionModel[];
}