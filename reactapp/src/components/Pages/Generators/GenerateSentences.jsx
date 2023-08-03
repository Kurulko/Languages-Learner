import React, { useState, Fragment } from 'react';
import { GenerateModelsByLanguage } from './GenerateModelsByLanguage';
import BaseInput from '../../Elements/BaseInput';

export default function GenerateSentences() {

    const [sentenceOptions, setSentenceOptions] = useState({
        rule: null,
        words: [],
    });

    function EditSentence(){

        function handleSentenceOptions(event) {
            const {name, value} = event.target;
            setSentenceOptions({ ...sentenceOptions, [name]: value })    
        }
        
        function handleWordInputChange(index, value) {
            const newWords = [...sentenceOptions.words];
            newWords[index] = value;
            setSentenceOptions({ ...sentenceOptions, "words": newWords });
        };
        
        function handleAddWordField() {
            setSentenceOptions({ ...sentenceOptions, "words": [...sentenceOptions.words, ''] });
        };
    
        function handleRemoveWordField(index) {
            const newWords = [...sentenceOptions.words];
            newWords.splice(index, 1);
            setSentenceOptions({ ...sentenceOptions, "words": newWords });
        };

        return <Fragment>
            <div>
                <label>Rule</label>
                <BaseInput value={sentenceOptions.rule} name="rule"
                            type="text" onChange={handleSentenceOptions} placeholder="Enter rule"/>
            </div> 
            <br />
            <div>
                <label>Words</label>
                <br />
                <div className="row">
                    <div className="col-md-3"></div>
                    <div className="col-md-9">
                        <button onClick={handleAddWordField} className="btn btn-primary" type="button">Add Word</button>
                    </div>
                </div>
                <br />
                {sentenceOptions.words.map((value, index) => (
                    <div key={index}>
                        <div className="row">
                            <div className="col-md-3"></div>
                            <div className="col-md-6">
                                <BaseInput value={value}
                                    type="text" onChange={(e) => handleWordInputChange(index, e.target.value)} />
                            </div>
                            <div className="col-md-3">
                                <button onClick={() => handleRemoveWordField(index)} className="btn btn-warning" type="button">Remove</button>
                            </div>
                        </div>
                        <br />
                    </div>
                ))}
            </div>
        </Fragment>;
    }

    return <GenerateModelsByLanguage name='Sentences' partPathToGet='sentences' pathToEdit='sentences-by-languages' redirectTo='sentences' 
                EditModel={EditSentence} modelsOptions={sentenceOptions}/>
}

