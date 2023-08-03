import React, { useState, createContext } from 'react';
import { variables } from '../../helpers/variables';

export const LanguageContext = createContext();

export function LanguageProvider(props) {
  const [currentLanguage, setCurrentLanguage] = useState(variables.DEFAULT_LANGUAGE_NAME);

  return (
    <LanguageContext.Provider value={{ currentLanguage, setCurrentLanguage }}>
      {props.children}
    </LanguageContext.Provider>
  );
}
