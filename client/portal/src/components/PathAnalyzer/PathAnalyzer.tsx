import { useDispatch, useSelector } from "react-redux";
import {
  analyzeMap,
  selectCurrent,
  textUpdated,
} from "../../features/currentMap";
import { saveMap, selectSaved } from "../../features/savedMaps";
import { AppDispatch } from "../../store";
import * as Styled from "./PathAnalyzer.styles";

const PathAnalyzer = () => {
  const current = useSelector(selectCurrent);
  const saved = useSelector(selectSaved);

  const dispatch = useDispatch<AppDispatch>();

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
      <Styled.TextArea
        rows={6}
        onChange={(e) => dispatch(textUpdated(e.target.value))}
      />
      <Styled.ButtonsWrapper>
        <Styled.Button disabled={current.analyzed} onClick={() => dispatch(analyzeMap())}>
          Analyze Map
        </Styled.Button>
        <Styled.Button onClick={() => dispatch(saveMap())}>
          Save Map
        </Styled.Button>
      </Styled.ButtonsWrapper>
      {current && current.analyzed && current.valid && (
        <>
          <div>Letters: {current.letters}</div>
          <div>Path: {current.pathAsCharacters}</div>
        </>
      )}
      {current && !current.valid && <div>{current.errorMessage}</div>}
      <div>{saved?.length}</div>
    </Styled.Container>
  );
};

export default PathAnalyzer;
