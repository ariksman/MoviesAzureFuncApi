import * as React from "react";
import {
    Card,
    CardImg,
    CardText,
    CardBody,
    CardTitle,
    CardSubtitle,
    Button,
} from "reactstrap";

interface MovieItemProps {
    name: string;
    description: string;
    id: string;
    imageUrl: string;
    homepageUrl: string;
    clickHandler: (event: string) => void;
}

export function MovieItemComponent(props: MovieItemProps) {
    
    const raisePageLinkClicked = (url: string) => {
        window.open(url, "_blank");
    };

    return (
        <div>
            <Card>
                <CardImg top width="50%" src={props.imageUrl} alt="movie image" />
                <CardBody>
                    <CardTitle>{props.name}</CardTitle>
                    <CardSubtitle>Description</CardSubtitle>
                    <CardText>{props.description}</CardText>
                    <Button
                        onClick={() => {
                            raisePageLinkClicked(props.homepageUrl);
                        }}
                    >Link
                    </Button>
                    <Button
                        onClick={() => {
                            props.clickHandler(props.id);
                        }}>
                        Remove
                    </Button>
                </CardBody>
            </Card>
        </div>
    );
}
