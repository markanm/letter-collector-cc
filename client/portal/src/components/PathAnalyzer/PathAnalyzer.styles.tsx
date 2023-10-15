import styled from "styled-components";

export const Container = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: center;
  width: 100%;
  max-width: 600px;
  height: 100%;
  border: 1px solid #fff;
  text-align: center;
`;

export const Title = styled.h1`
  color: #cecece;
`;

export const Subtitle = styled.h2`
  color: #cecece;
  font-style: italic;
  margin: 0;
`;

export const RuleList = styled.ul`
  text-align: left;
`;

export const TextArea = styled.textarea`
  border: 1px solid #fff;
  width: 400px;
  padding: 1rem;
`;

export const Button = styled.button`
  margin-top: 2rem;
  font-size: 1rem;
`;
