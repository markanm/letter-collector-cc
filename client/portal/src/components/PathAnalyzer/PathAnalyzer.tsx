import { useState } from "react";
import { analyzeService } from "../../services/AnalyzerService/AnalyzerService";
import * as Styled from "./PathAnalyzer.styles";

const PathAnalyzer = () => {
  const [mapText, setMapText] = useState("");
  const [result, setResult] = useState({
    letters: "",
    pathAsCharacters: "",
  });

  const analyzePath = async () => {
    setResult(await analyzeService.analyzePath(mapText.split("\n")));
  };

  return (
    <Styled.Container>
      <Styled.Title>Letter Collector</Styled.Title>
      <Styled.Subtitle>Rules:</Styled.Subtitle>
      <Styled.RuleList>
        <li>Start at the character @</li>
        <li>Follow the path</li>
        <li>Collect letters</li>
        <li>Stop when you reach the character x</li>
      </Styled.RuleList>
      <Styled.TextArea rows={6} onChange={(e) => setMapText(e.target.value)} />
      <Styled.Button onClick={() => analyzePath()}>Analyze</Styled.Button>
      {result && (
        <>
          <div>Letters: {result.letters}</div>
          <div>Path: {result.pathAsCharacters}</div>
        </>
      )}
    </Styled.Container>
  );
};

export default PathAnalyzer;
