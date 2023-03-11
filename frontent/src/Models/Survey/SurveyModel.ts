import Category from "../Category/Category";
import { SurveyOptionWithUsers } from "./SurveyOptionWithUsers";

export interface SurveyModel {
    id: number;
    isTheMaker: boolean;
    question: string;
    isAnswered: boolean;
    answerId: number;
    totalAnswers: number;
    categories: Category[]
    optionWithUsers: SurveyOptionWithUsers[];
}
