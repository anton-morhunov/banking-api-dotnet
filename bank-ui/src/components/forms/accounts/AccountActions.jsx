import BlockButton from "../../ui/Button/BlockButton.jsx";
import UnblockButton from "../../ui/Button/UnblockButton.jsx";
import {actionsButtonsStyle, actionsContainerStyle, actionsTitleStyle} from "../../../styles/accountDetailsStyle.js";

function AccountActions({status, onChangeStatus}) {
    
    return(
        <div
            style={actionsContainerStyle}
        >
            <h3
                style={actionsTitleStyle}
            >
                Actions
            </h3>
            <div
                style={actionsButtonsStyle}
            >
                {
                    status === 0 ? (
                        <>
                            <BlockButton
                                onClick={() => onChangeStatus(1)}
                            >
                                Block
                            </BlockButton>

                            <BlockButton
                                onClick={() => onChangeStatus(2)}
                            >
                                Suspend
                            </BlockButton>
                        </>
                    ) : (
                        <UnblockButton
                            onClick={() => onChangeStatus(0)}
                        >
                            Unblock
                        </UnblockButton>
                    )
                }
            </div>
        </div>
    )
}
export default AccountActions;